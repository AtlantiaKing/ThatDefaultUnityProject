namespace That
{
	public enum LockAxis : byte
	{
		None = 0x00,
		X = 0x01,
		Y = 0x02,
		Z = 0x04,
		XY = 0x03,
		YZ = 0x06,
		XZ = 0x05,
		All = 0x07
	}
	public static class LockAxisHelper
	{
		public static bool IsLockedOn(this LockAxis thisAxis, LockAxis axis)
		{
			return (thisAxis & axis) == axis;
		}

		public static LockAxis LockOn(this LockAxis thisAxis, LockAxis axis)
		{
			return thisAxis | axis;
		}
	}
}
