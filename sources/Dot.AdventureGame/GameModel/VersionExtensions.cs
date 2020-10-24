using System;

namespace DustInTheWind.Dot.AdventureGame.GameModel
{
    internal static class VersionExtensions
    {
        public static bool Equals(this Version version1, Version version2, int fieldCount)
        {
            if (fieldCount <= 0) throw new ArgumentOutOfRangeException(nameof(fieldCount));

            bool result = version1.Major == version2.Major;

            if (fieldCount == 1)
                return result;

            result &= version1.Minor == version2.Minor;

            if (fieldCount == 2)
                return result;

            result &= version1.Revision == version2.Revision;

            if (fieldCount == 3)
                return result;

            result &= version1.Build == version2.Build;

            return result;
        }

        public static int CompareTo(this Version version1, Version version2, int fieldCount)
        {
            if (fieldCount <= 0)
                return 0;

            int result = version1.Major.CompareTo(version2.Major);

            if (result != 0 || fieldCount == 1)
                return result;

            result = version1.Minor.CompareTo(version2.Minor);

            if (result != 0 || fieldCount == 2)
                return result;

            result = version1.Revision.CompareTo(version2.Revision);

            if (result == 0 || fieldCount == 3)
                return result;

            result = version1.Build.CompareTo(version2.Build);

            return result;
        }
    }
}