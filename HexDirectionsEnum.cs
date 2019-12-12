using System.Collections.Generic;
using UnityEngine;

public class HexDirectionsEnum {

	/*
	 * This Class emulates Java Enums, built for simplicity.
	 * Enums in C# do not have the expansive functionality like Java enums.
	 * Building enum data into a class like this allows for more data to be stored, and allows for this class to perform operations on its own.
	 */







	// --- Sub-Class used for holding the directional data.
	public class HexDirEnum : HexDirectionsEnum {

		private int xOffset, yOffset;
		private string name, container, sprite;
		/* --- 'xOffset' & 'yOffset' hold (+/-)1 or 0 to indicate the direction they are reletive to of a grid co-ordinate.
		 * --- 'name' holds the given name of the direction, used for printing to screen or debugging.
		 * --- 'container' holds the name of the gameObject containing the directional sprite for that co-ordinate.
		 * --- 'sprite' holds the name of the actual sprite component, in order to toggle the renderer.
		*/




		// --- Class constructor declaration.
		public HexDirEnum(string name, string container, string sprite, int xOffset, int yOffset) {
			this.xOffset = xOffset;
			this.yOffset = yOffset;
			this.name = name;
			this.container = container;
			this.sprite = sprite;
		}

		// --- Getters:
		public int getXOffset() {
			return xOffset;
		}

		public int getYOffset() {
			return yOffset;
		}

		public string getEnumName() {
			return name;
		}

		public string getEnumContainerName() {
			return container;
		}

		public string getEnumSpriteName() {
			return sprite;
		}
	}



	// --- Class constructor declaration.
	public HexDirectionsEnum() {

	}




	// ---------------------- STATIC METHODS ----------------------


	// --- Begin defining the directions as the Sub-Class.
	public static HexDirEnum
		NORTH_WEST = new HexDirEnum("NorthWest", "NWest", "LineNW", -1, 1),
		NORTH_EAST = new HexDirEnum("NorthEast", "NEast", "LineNE", 0, 1),
		SOUTH_WEST = new HexDirEnum("SouthWest", "SWest", "LineSW", 0, -1),
		SOUTH_EAST = new HexDirEnum("SouthEast", "SEast", "LineSE", 1, -1),
		WEST = new HexDirEnum("West", "West", "LineW", -1, 0),
		EAST = new HexDirEnum("East", "East", "LineE", 1, 0);





	// --- Include defined sub-class directions in a list for use in iterations.
	private readonly static List<HexDirEnum> directionsList = new List<HexDirEnum> {
			NORTH_WEST,
			NORTH_EAST,
			WEST,
			EAST,
			SOUTH_WEST,
			SOUTH_EAST
		};




	// --- Convert all sub-class directions to HexDirectionsEnum class types, and return in a list.
	public static List<HexDirectionsEnum> getAll() {

		List<HexDirectionsEnum> returnList = new List<HexDirectionsEnum> { };

		foreach (HexDirEnum direction in directionsList)
			returnList.Add(direction);

		return returnList;
	}




	// --- Gets the opposite direction of any given direction.
	public static HexDirectionsEnum getOppositeDirection(HexDirectionsEnum direction) {

		if (direction == null)
			throw new System.Exception("getOppositeDirection was passed a NULL value.");

		switch (direction.getName()) {
			case "NorthWest":
			return SOUTH_EAST;
			case "NorthEast":
			return SOUTH_WEST;
			case "SouthWest":
			return NORTH_EAST;
			case "SouthEast":
			return NORTH_WEST;
			case "West":
			return EAST;
			case "East":
			return WEST;
			default:
			// #If this ever triggers, all hope is lost.#
			throw new System.Exception("getOppositeDirection was not passed a valid direction: " + direction.getName());
		}

	}




	// --- Calculate the direction a co-ordinate is from another adjacent co-ordinate.
	public static HexDirectionsEnum calcDirFromAdjCoOrds(CoOrd coOrd1, CoOrd coOrd2) {

		foreach (HexDirEnum direction in directionsList) {
			if (coOrd1.getX() + direction.getX() == coOrd2.getX() && coOrd1.getY() + direction.getY() == coOrd2.getY())
				return direction;
		}

		// --- No valid direction found, co-ordinates are probably not adjacent.
		Debug.Log("WARN: calcDirFromAdjCoOrds did not find valid direction between: "+coOrd1.ToString() + "  &  "+coOrd2.ToString());
		return null;
	}







	// ---------------------- INSTANCE METHODS ----------------------


	public string getName() {
		return ((HexDirEnum) this).getEnumName();
	}

	public string getContainerName() {
		return ((HexDirEnum) this).getEnumContainerName();
	}

	public string getSpriteName() {
		return ((HexDirEnum) this).getEnumSpriteName();
	}

	public int getX() {
		return ((HexDirEnum) this).getXOffset();
	}

	public int getY() {
		return ((HexDirEnum) this).getYOffset();
	}

	public HexDirectionsEnum getOppositeDirection() {
		return getOppositeDirection(this);
	}

	public int ordinate() {
		return directionsList.IndexOf((HexDirEnum) this);
	}

	public override string ToString() {
		return getName() + " : [" + getX() + "," + getY() + "]";
	}
}
