using System;
using System.Collections.Generic;
using UnityEngine;

namespace XrMotionDataExplorer.Data
{
    public class MotionDataLoader : MonoBehaviour
    {
        [SerializeField] private TextAsset motionDataCsv;
        public IReadOnlyList<SpatialSample> Dataset { get; private set; }

        private void Awake()
        {
            if (motionDataCsv == null)
                throw new NullReferenceException("motionDataCsv is null");
            try
            {
                Dataset = DataParser.ParseMotionData(motionDataCsv.text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
