using System;
using System.Collections.Generic;

namespace Engine
{
    /// <summary>
    /// Class <c>Sector</c> represents the entire game map.
    /// </summary>
    public class Sector
    {
        /// <summary>
        /// a list of every <c>StarSystem</c> in the sector.
        /// </summary>
        public List<StarSystem> StarSystems { get; set; }
        /// <summary>
        /// the height and width of the sector in lightyears.
        /// </summary>
        public int SectorSize { get; set; } = 100;
        /// <summary>
        /// the number of systems in the sector.
        /// </summary>
        public int NumStarSystems { get; set; } = 20;
        /// <summary>
        /// the amount of precision to be used when placing systems.
        /// </summary>
        /// <remarks>
        /// A higher precision will allow more positions to place systems.
        /// </remarks>
        private const int WeightMapPrecision = 100;
        /// <summary>
        /// the scale that is passed to the perlin noise generator.
        /// </summary>
        /// <remarks>
        /// A higher scale results in more "jagged" results.
        /// A lower scale results in more "smooth" results.
        /// </remarks>
        private const float NoiseScale = 10;
        /// <summary>
        /// the maximum allowed size of system in this sector.
        /// </summary>
        /// <remarks>
        /// The size of systems is distributed evenly.
        /// </remarks>
        private const int MaxSystemSize = 10;



        /// <summary>
        /// Initializes an instance of <c>StarSystem</c> with default
        /// parameters.
        /// </summary>
        public Sector()
        {
            StarSystems = new List<StarSystem>();
        }

        /// <summary>
        /// Initializes an instance of <c>StarSystem</c> with the specified
        /// sectorSize.
        /// </summary>
        /// <param name="sectorSize">
        /// the height and width of the sector in lightyears.
        /// </param>
        public Sector(int sectorSize)
        {
            StarSystems = new List<StarSystem>();
            SectorSize = sectorSize;
            NumStarSystems = SectorSize / 5;
        }

        /// <summary>
        /// Initializes an instance of <c>StarSystem</c> with the specified
        /// sectorSize and number of star systems.
        /// </summary>
        /// <param name="sectorSize">
        /// the height and width of the sector in lightyears.
        /// </param>
        /// <param name="numStarSystems">
        /// the number of systems in the sector.
        /// </param>
        public Sector(int sectorSize, int numStarSystems)
        {
            StarSystems = new List<StarSystem>();
            SectorSize = sectorSize;
            NumStarSystems = numStarSystems;
        }


        /// <summary>
        /// Instance method <c>GenerateSystems</c> creates every
        /// <c>StarSystem</c> in the game based on the configuration of
        /// <c>Sector</c>.
        /// </summary>
        public void GenerateSystems()
        {
            // Find the length and width of the weight map
            int mapSize = SectorSize * WeightMapPrecision;

            // Create a blank weight map for the perlin noise generator
            float[,] weightMap = new float[mapSize, mapSize];

            // We want an even distribution of planets so we give each tile an equal weight
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    weightMap[i, j] = 1f;
                }
            }

            // Call the perlin noise generator
            float[,] noiseMap = NoiseMapGenerator.GenerateNoiseMap(weightMap, NoiseScale);

            // Any additional post-processing goes here

            // For each system, find the largest point in the noise map and place a system  
            int numGameObjectTypes = Enum.GetNames(typeof(StellarClass)).Length;
            for (int s = 0; s < NumStarSystems; s++)
            {
                float bestValue = 0;
                int bestX = 0, bestY = 0;

                for (int i = 0; i < mapSize; i++)
                {
                    for (int j = 0; j < mapSize; j++)
                    {
                        if (noiseMap[i, j] > bestValue)
                        {
                            bestX = i;
                            bestY = j;
                            bestValue = noiseMap[i, j];
                        }
                    }
                }

                // Determine new system's star class
                StellarClass starClass = (StellarClass)GameManager.GameRandom.Next(numGameObjectTypes);
                // Determine new system's size
                int systemSize = GameManager.GameRandom.Next(MaxSystemSize) + 1;
                // Determine new system's minerals
                int minerals = GameManager.GameRandom.Next(5) + 1;
                // Place the system
                StarSystem newSystem = new StarSystem(starClass, systemSize,
                                                      bestX / (float)WeightMapPrecision,
                                                      bestY / (float)WeightMapPrecision,
                                                      minerals);
                StarSystems.Add(newSystem);

                // Prevent other systems from spawning too close
                int systemSpacing = (int)Math.Floor((float)mapSize / (float)NumStarSystems / 2f) * 2;
                for (int i = 0 - systemSpacing; i < systemSpacing; i++)
                {
                    for (int j = 0 - systemSpacing; j < systemSpacing; j++)
                    {
                        int targetX = bestX + i, targetY = bestY + j;
                        if (targetX >= 0 && targetX < mapSize &&
                            targetY >= 0 && targetY < mapSize)
                        {
                            noiseMap[targetX, targetY] = 0;
                        }
                    }
                }
            }
        }
    }
}
