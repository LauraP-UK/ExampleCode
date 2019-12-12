using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Directional data for Hexagonal Grid.
/// </summary>
/// 
/// <remarks>
/// This Class emulates Java Enums.
/// Enums in C# do not have the expansive functionality of Java enums.
/// Building enum data into a class like this allows for more data to be stored, and allows for this class to handle both instance and static operations.
/// 
/// The purpose of this class is to hold instance and static information about the 6 different directions applicable to a 
/// hexagonal tiled grid, where the hexagons are positioned so that lines of them are parallel along the East-West (Left-Right) direction. 
/// Those directions being; EAST, SOUTH-EAST, SOUTH-WEST, WEST, NORTH-WEST, NORTH-EAST
/// 
/// Visual example of grid:
/// 
/// |  * * * * * * * *
/// | * * * * * * * * *
/// |  * * * * * * * *
/// | * * * * * * * * *
/// |  * * * * * * * *
/// 
/// All data in this class is read-only after initial setup at run time.
/// </remarks>


public class HexDirectionsEnum {


	/// <summary>
	/// HexDirectionsEnum Child class used for holding the directional data.
	/// </summary>
	public class HexDirEnum : HexDirectionsEnum {

		/// <summary>
		/// Value is -1, 0, or 1 depending on specific direction.
		///<para>Representative of the difference between two adjacent co-ordinates, positioned in the respective direction from each other.</para>
		/// </summary>
		private readonly int xOffset, yOffset;

		/// <summary>
		/// The given name of the direction, for Strings or debugging.
		/// </summary>
		private readonly string name;

		/// <summary>
		/// The name of the gameObject containing the directional sprite for respective direction.
		/// </summary>
		private readonly string container;

		/// <summary>
		/// The name of the sprite component.
		/// </summary>
		private readonly string sprite;
		
		/*
		 * --- NB: 'container' & 'sprite' serve a purpose for a specific project and can be removed for generic use.
		*/
		


		/// <summary>
		/// HexDirectionsEnum - Child class constructor.
		/// </summary>
		/// <param name="name">Printable name of the direction.</param>
		/// <param name="container">gameObject name containing the directional sprite.</param>
		/// <param name="sprite">Sprite component name.</param>
		/// <param name="xOffset">X Co-Ordinate difference.</param>
		/// <param name="yOffset">Y Co-Ordinate difference.</param>
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


	/// <summary>
	/// Class Constructor.
	/// </summary>
	public HexDirectionsEnum() {
		// --- Constructor logic goes here if needed.
	}




	// ---------------------- STATIC METHODS ----------------------


	// --- Begin defining the directions as the HexDirEnum child class.
	/// <summary>
	/// Hexagonal Grid direction.
	///<para>Contains info on; Name, X and Y directional difference, and Sprite name.</para>
	/// </summary>
	public readonly static HexDirEnum
		NORTH_WEST = new HexDirEnum("NorthWest", "NWest", "LineNW", -1, 1),
		NORTH_EAST = new HexDirEnum("NorthEast", "NEast", "LineNE", 0, 1),
		SOUTH_WEST = new HexDirEnum("SouthWest", "SWest", "LineSW", 0, -1),
		SOUTH_EAST = new HexDirEnum("SouthEast", "SEast", "LineSE", 1, -1),
		WEST = new HexDirEnum("West", "West", "LineW", -1, 0),
		EAST = new HexDirEnum("East", "East", "LineE", 1, 0);





	// --- Include defined HexDirEnum child class directions in a list for use in iterations.
	/// <summary>
	/// List of all HexDirEnum directions.
	/// </summary>
	private readonly static List<HexDirEnum> directionsList = new List<HexDirEnum> {
			NORTH_WEST,
			NORTH_EAST,
			WEST,
			EAST,
			SOUTH_WEST,
			SOUTH_EAST
		};




	// --- Convert all sub-class directions to HexDirectionsEnum class types, and return in a list.
	/// <summary>
	/// Gets all HexDirEnum directions as a list of HexDirectionsEnum.
	/// </summary>
	/// <returns>Returns a List of all HexDirectionsEnum directions.</returns>
	public static List<HexDirectionsEnum> getAll() {

		List<HexDirectionsEnum> returnList = new List<HexDirectionsEnum> { };

		foreach (HexDirEnum direction in directionsList)
			returnList.Add(direction);

		return returnList;
	}



	/// <summary>
	/// Gets the Ordinal value of the given direction.
	/// </summary>
	/// <param name="direction">The direction to get the Ordinal of.</param>
	/// <returns>Returns an int containing the index of the given direction in the directionsList.</returns>
	public static int ordinal(HexDirectionsEnum direction) {
		return directionsList.IndexOf((HexDirEnum) direction);
	}




	// --- Gets the opposite direction of any given direction. Having this both static and instance-specific allows greater usability in projects.
	/// <summary>
	/// Gets the opposite direction of the given argument direction.
	/// </summary>
	/// <example>
	/// If the Argument is WEST, the return value is EAST.
	/// </example>
	/// <param name="direction">The direction to get the opposite of.</param>
	/// <returns>The HexDirectionsEnum direction opposite to the direction given.</returns>
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
	/// <summary>
	/// Gets the direction co-ordinate 'A' is from another adjacent co-ordinate 'B'.
	/// <para>Example: Co-Ordinate 'B' is NORTH-WEST of Co-Ordinate 'A'.</para>
	/// </summary>
	/// <param name="coOrdFrom">The Co-Ordinate 'A'.</param>
	/// <param name="coOrdTo">The Co-Ordinate 'B'.</param>
	/// <returns>
	/// Returns a HexDirectionsEnum direction, describing the co-ordinal relationship between point 'A' and 'B'.
	/// <para>Returns NULL if co-ordinates are not adjacent.</para>
	/// </returns>
	public static HexDirectionsEnum calcDirFromAdjCoOrds(CoOrd coOrdFrom, CoOrd coOrdTo) {

		foreach (HexDirEnum direction in directionsList) {
			if (coOrdFrom.getX() + direction.getX() == coOrdTo.getX() &&
				coOrdFrom.getY() + direction.getY() == coOrdTo.getY())
				return direction;
		}

		// --- No valid direction found, co-ordinates are probably not adjacent.
		Debug.Log("WARN: calcDirFromAdjCoOrds did not find valid direction between: "+coOrdFrom.ToString() + "  &  "+coOrdTo.ToString());
		return null;
	}







	// ---------------------- INSTANCE METHODS ----------------------

	/// <summary>
	/// Get the name of this Direction.
	/// </summary>
	/// <returns>Returns a String containing the direction's name.</returns>
	public string getName() {
		return ((HexDirEnum) this).getEnumName();
	}

	/// <summary>
	/// Get the name of the gameObject containing the Sprite for this direction.
	/// </summary>
	/// <returns>Returns a String containing the gameObject's name.</returns>
	public string getContainerName() {
		return ((HexDirEnum) this).getEnumContainerName();
	}

	/// <summary>
	/// Get the name of the Sprite for this direction.
	/// </summary>
	/// <returns>Returns a String containing the Sprite's name.</returns>
	public string getSpriteName() {
		return ((HexDirEnum) this).getEnumSpriteName();
	}

	/// <summary>
	/// Get the X Co-Ordinate offset for this direction.
	/// </summary>
	/// <returns>Returns an int containing the X-Offset.</returns>
	public int getX() {
		return ((HexDirEnum) this).getXOffset();
	}

	/// <summary>
	/// Get the Y Co-Ordinate offset for this direction.
	/// </summary>
	/// <returns>Returns an int containing the Y-Offset.</returns>
	public int getY() {
		return ((HexDirEnum) this).getYOffset();
	}

	/// <summary>
	/// Get the opposite direction for this direction.
	/// </summary>
	/// <returns>Returns a HexDirectionsEnum direction opposite to this direction.</returns>
	public HexDirectionsEnum getOppositeDirection() {
		return getOppositeDirection(this);
	}

	/// <summary>
	/// Get the Ordinal value of this direction.
	/// </summary>
	/// <returns>Returns an int containing the index of this direction in the directionsList.</returns>
	public int ordinal() {
		return ordinal(this);
	}

	/// <summary>
	/// An Override: Convert this direction into formatted readable String data.
	/// </summary>
	/// <returns>Returns the data of this direction in a concatinated String.</returns>
	public override string ToString() {
		return getName() + " : [" + getX() + "," + getY() + "]";
	}

	/// <summary>
	/// An Override: Checks if this direction is the same as a given direction.
	/// </summary>
	/// <param name="obj">The direction to check against.</param>
	/// <returns>
	/// Returns TRUE if both directions share the same name.
	/// <para>Returns FALSE if obj is not of type HexDirectionsEnum, or names are not the same.</para>
	/// </returns>
	public override bool Equals(object obj) {
		var direction = obj as HexDirectionsEnum;
		return direction != null &&
			direction.getName() == getName();
	}

	/// <summary>
	/// An Override: Gets the HashCode, currently doesn't modify any behaviour.
	/// </summary>
	/// <returns>Returns the normal int HashCode.</returns>
	public override int GetHashCode() {
		return base.GetHashCode();
	}
}
