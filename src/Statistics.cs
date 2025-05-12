using System.Collections.Concurrent;

internal class Statistics
{
    /// <summary>
    /// Analyzes a queue of values to detect if there's an increasing trend.
    /// Uses linear regression to calculate the slope of the trend line.
    /// Works with any numeric type that can be converted to double.
    /// </summary>
    /// <typeparam name="T">The numeric type stored in the queue</typeparam>
    /// <param name="queue">Queue of values to analyse</param>
    /// <param name="converter">Optional function to convert T to double. If null, Convert.ToDouble will be used.</param>
    /// <param name="trendThreshold">Percentage threshold to consider a trend as increasing (default: 30%)</param>
    /// <returns>
    /// A tuple containing:
    /// - Whether there is a significant increasing trend (bool)
    /// - The percentage increase over the window (double)
    /// - The slope of the trend line (double)
    /// </returns>
    internal static (bool isIncreasing, double percentageIncrease, double slope) AnalyzeValueTrend<T>(ConcurrentQueue<T> queue, Func<T, double>? converter = null, double trendThreshold = 30)
    {
        // Need at least 3 data points for a meaningful trend
        if (queue.Count < 3)
        {
            return (false, 0, 0);
        }

        // Default converter uses Convert.ToDouble
        converter ??= value => Convert.ToDouble(value);

        // Convert values to double array
        var values = queue.Select(converter).ToArray();
        int n = values.Length;

        // Simple linear regression to find slope
        // y = mx + b where m is the slope
        double sumX = 0;
        double sumY = 0;
        double sumXY = 0;
        double sumX2 = 0;

        for (int i = 0; i < n; i++)
        {
            sumX += i;
            sumY += values[i];
            sumXY += i * values[i];
            sumX2 += i * i;
        }

        double avgX = sumX / n;
        double avgY = sumY / n;

        // Calculate slope (m)
        double slope = 0;
        double denominator = sumX2 - n * avgX * avgX;

        if (Math.Abs(denominator) > 0.0001) // Avoid division by zero
        {
            slope = (sumXY - n * avgX * avgY) / denominator;
        }

        // Calculate percentage increase over the window
        double firstValue = values.First();
        double lastValue = values.Last();

        // Avoid division by zero
        double percentageIncrease = 0;
        if (Math.Abs(firstValue) > 0.0001)
        {
            percentageIncrease = (lastValue - firstValue) / firstValue * 100;
        }

        // Determine if there's a significant increasing trend
        // Consider it significant if:
        // 1. The slope is positive and
        // 2. The percentage increase is substantial (e.g., more than the specified threshold)
        bool isIncreasing = slope > 0 && percentageIncrease > trendThreshold;

        return (isIncreasing, percentageIncrease, slope);
    }
}