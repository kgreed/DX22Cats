namespace DX22Cats.Win.Editors
{
    internal class HandyXAFFunctions
    {
        internal static void WarnRecordsCountIfNeeded(int recordCount)
        {
            var maxCount = 1000;
            if (recordCount == maxCount)
            {
                MessageBox.Show(@$"There are more than {maxCount} records available, but only {maxCount} can be shown. 
                    Consider refining your filter or increasing the configuration");
            }
        }
    }
}