using System;

public enum OrbColorEnum
{
	BLACK,
	BLUE,
	GREEN,
	RED,
	WHITE,
	YELLOW
}

public enum OrbSymbolEnum
{
	CIRCLE,
	SQUARE,
	TRIANGLE,
	STAR
}

public enum RingEnum
{
	OUTER,
	INNER,
	ORIGIN
}

public enum DirectionEnum
{
	NORTH,
	NORTHEAST,
	EAST,
	SOUTHEAST,
	SOUTH,
	SOUTHWEST,
	WEST,
	NORTHWEST,
	MIDDLE

}

[Serializable]
public class OrbType
{
	public OrbColorEnum Color;
//	public OrbSymbolEnum Symbol;

	public OrbType(OrbColorEnum color)
	{
		Color = color;
	}

    public OrbType GetRandomOrbType()
    {
  		OrbColorEnum[] values = (OrbColorEnum[]) Enum.GetValues(typeof(OrbColorEnum));
		OrbType result = new OrbType(values[new Random().Next(0,values.Length)]);

  		return result;
	}    

	public string GetResourceName(bool active)
	{
		string colorName = this.Color.ToString().ToLower();
		string capitalized = char.ToUpper(colorName[0]) + colorName.Substring(1);

		return string.Format("OrbSprites/Orb_{0}_{1}", capitalized, active ? "Active" : "Inactive");

	}
}

[Serializable]
public class OrbPosition
{
	public RingEnum Ring;
	public DirectionEnum Direction;

	public OrbPosition(RingEnum ring, DirectionEnum direction)
	{
		if (ring == RingEnum.ORIGIN)
		{
			Ring = RingEnum.ORIGIN;
			Direction = DirectionEnum.MIDDLE;
		}
		else
		{
			Ring = ring;
			Direction = direction;
		}
	}
}

public class Orb
{
	public OrbType Type;
	public OrbPosition Position;

	public Orb(OrbType type, OrbPosition position)
	{
		Position = position;
		Type = type;
	}

}

