using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class ColorManager
{
    private static readonly Dictionary<string, string> colorCode = new()
    {
        {"grey", "808080FF"},
        {"blue1", "DCFBFFFF"},
        {"blue2", "3399FFFF"},
        {"blue3", "0066CCFF"},
        {"blue4", "004C99FF"}
    };

    /// <summary>
    /// get color to display book progress in short
    /// </summary>
    /// <param name="page_cnt">
    /// number of pages being read (0: not read; 1 or more (<= 10): in progress or read)
    /// </param>
    /// <param name="min_read_times">
    /// The number of times the least read page has been read among the target pages.
    /// </param>
    /// <returns>
    /// color code
    /// </returns>
    public static string GetColorCodeInShort(int page_cnt, int min_read_times)
    {
        if (page_cnt == 0)
        {
            return colorCode["grey"];
        }

        return min_read_times switch
        {
            0 => colorCode["blue1"],
            1 => colorCode["blue2"],
            2 => colorCode["blue3"],
            _ => colorCode["blue4"],
        };
    }

    /// <summary>
    /// get color to display book progress page by page
    /// </summary>
    /// <param name="progressNum">
    /// The number of times the page has been read.
    /// </param>
    /// <returns>
    /// color code
    /// </returns>
    public static string GetColorCodeProgress(int progressNum)
    {
        return progressNum switch
        {
            0 => colorCode["grey"],
            1 => colorCode["blue2"],
            2 => colorCode["blue3"],
            _ => colorCode["blue4"],
        };
    }
}
