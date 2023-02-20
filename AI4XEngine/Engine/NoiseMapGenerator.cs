using DotnetNoise;

namespace Engine
{
    /// <summary>
    /// Static class <c>NoiseMapGenerator</c> contains the methods needed to
    /// generate 2-dimensional perlin noise graphs.
    /// </summary>
    public static class NoiseMapGenerator
    {
        /// <summary>
        /// Generates a weighted, randomized, rectangular perlin noise graph
        /// using the given weight map and scale.
        /// </summary>
        /// <param name="weightMap">
        /// a 2-dimensional array of floats representing how likely a larger
        /// number should appear in each position.
        /// </param>
        /// <param name="scale">
        /// an int representing the scale to be passed to the perlin noise
        /// sampler.
        /// </param>
        /// <remarks>
        /// A higher scale results in more "jagged" results.
        /// A lower scale results in more "smooth" results.
        /// </remarks>
        /// <returns>
        /// a 2-dimensional array of weighted perlin noise of the same size
        /// as the given weight map.
        /// </returns>
        public static float[,] GenerateNoiseMap(float[,] weightMap, float scale)
        {
            // Find the depth and width assuming a rectangular map
            int mapWidth = weightMap.GetLength(0);
            int mapDepth = weightMap.GetLength(1);

            // Create an empty noise map with the the same size as the weightMap
            float[,] noiseMap = new float[mapDepth, mapWidth];
            float sampleXOffset = (float)GameManager.GameRandom.NextDouble() * 100;
            float sampleZOffset = (float)GameManager.GameRandom.NextDouble() * 100;

            // Create our noise generator
            FastNoise noiseGenerator = new FastNoise();

            for (int zIndex = 0; zIndex < mapDepth; zIndex++)
            {
                for (int xIndex = 0; xIndex < mapWidth; xIndex++)
                {
                    float noise = 0;
                    if (weightMap[zIndex, xIndex] > 0)
                    {
                        // Calculate sample indices based on the coordinates and the scale
                        float sampleX = xIndex / scale + sampleXOffset;
                        float sampleZ = zIndex / scale + sampleZOffset;
                        // Generate noise value using PerlinNoise
                        noise = noiseGenerator.GetPerlin(sampleX, sampleZ) * weightMap[zIndex, xIndex];
                    }

                    noiseMap[zIndex, xIndex] = noise;
                }
            }

            return noiseMap;
        }
    }
}
