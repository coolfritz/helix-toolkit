﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderUtil.cs" company="Helix Toolkit">
//   Copyright (c) 2014 Helix Toolkit contributors
// </copyright>
// <summary>
//
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace HelixToolkit.Wpf.SharpDX
{
    using System.IO;
    using global::SharpDX;
    using global::SharpDX.Direct3D11;
    using Direct3D11 = global::SharpDX.Direct3D11;

    public static class RenderUtil
    {
#if SYSTEM_DRAWING
        /// <summary>
        /// 
        /// </summary>
        /// <param name="image"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this System.Drawing.Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                return ms.ToArray();
            }
        } 
#endif


        public static byte[] ToByteArray(this System.Windows.Media.Imaging.BitmapSource bitmapSource)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                var encoder = new System.Windows.Media.Imaging.BmpBitmapEncoder();
                encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(bitmapSource));
                encoder.Save(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Create buffer
        /// </summary>
        public static Direct3D11.Buffer CreateBuffer<T>(this Direct3D11.Device device, BindFlags flags, int sizeofT, T[] range, int length)
            where T : struct
        {
            return Direct3D11.Buffer.Create<T>(device, range, new BufferDescription()
            {
                BindFlags = flags,
                SizeInBytes = length * sizeofT,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                Usage = ResourceUsage.Default,
            });
        }

        /// <summary>
        /// Create buffer
        /// </summary>
        public static Direct3D11.Buffer CreateBuffer<T>(this Direct3D11.Device device, BindFlags flags, int sizeofT, T[] range)
            where T : struct
        {
            return CreateBuffer<T>(device, flags, sizeofT, range, range.Length);
        }
    }
}