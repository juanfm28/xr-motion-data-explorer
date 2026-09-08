using System;
using System.Collections.Generic;
using UnityEngine;

namespace XrMotionDataExplorer.Data
{
    public class MotionDataLoader : MonoBehaviour
    {
        [SerializeField] private TextAsset motionDataCsv;
        public IReadOnlyList<SpatialSample> dataset;

        private void Start()
        {
            if (motionDataCsv == null)
                throw new NullReferenceException("motionDataCsv is null");
            try
            {
                dataset = DataParser.ParseMotionData(motionDataCsv.text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
