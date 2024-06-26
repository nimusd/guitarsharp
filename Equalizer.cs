﻿using NAudio.Dsp;
using NAudio.Wave;

namespace Guitarsharp
{
    /// <summary>
    /// Basic example of a multi-band eq
    /// uses the same settings for both channels in stereo audio
    /// Call Update after you've updated the bands
    /// Potentially to be added to NAudio in a future version
    /// </summary>
    public class Equalizer : ISampleProvider
    {
        private ISampleProvider sourceProvider;
        public  EqualizerBand[] bands;
        private BiQuadFilter[,] filters;
        private int channels;
        public  int bandCount;
        private bool updated;

        public Equalizer(ISampleProvider sourceProvider, EqualizerBand[] bands)
        {
            this.sourceProvider = sourceProvider;
            this.bands = bands;
            channels = GlobalConfig.GlobalWaveFormat.Channels;
            bandCount = bands.Length;
            filters = new BiQuadFilter[channels, bands.Length];
            CreateFilters();
        }

        private void CreateFilters()
        {
            for (int bandIndex = 0; bandIndex < bandCount; bandIndex++)
            {
                var band = bands[bandIndex];
                for (int n = 0; n < channels; n++)
                {
                    if (filters[n, bandIndex] == null)
                        filters[n, bandIndex] = BiQuadFilter.PeakingEQ(GlobalConfig.GlobalWaveFormat.SampleRate, band.Frequency, band.Bandwidth, band.Gain);
                    else
                        filters[n, bandIndex].SetPeakingEq(GlobalConfig.GlobalWaveFormat.SampleRate, band.Frequency, band.Bandwidth, band.Gain);
                }
            }
        }

        public void Update()
        {
            updated = true;
            CreateFilters();
        }
        public List< float> Process(List<float> input)
        {

            if (updated)
            {
                CreateFilters();
                updated = false;
            }

            for (int n = 0; n < input.Count; n++)
            {
                int ch = n % channels;

                for (int band = 0; band < bandCount; band++)
                {
                    input[ n] = filters[ch, band].Transform(input[n]);
                }
            }
            return input;
        }
        public WaveFormat WaveFormat => sourceProvider.WaveFormat;

        public int Read(float[] buffer, int offset, int count)
        {
            int samplesRead = sourceProvider.Read(buffer, offset, count);

            if (updated)
            {
                CreateFilters();
                updated = false;
            }

            for (int n = 0; n < samplesRead; n++)
            {
                int ch = n % channels;

                for (int band = 0; band < bandCount; band++)
                {
                    buffer[offset + n] = filters[ch, band].Transform(buffer[offset + n]);
                }
            }
            return samplesRead;
        }
    }

    public class EqualizerBand
    {
        public int Frequency { get; set; }
        public float Gain { get; set; }
        public float Bandwidth { get; set; }
    }
}