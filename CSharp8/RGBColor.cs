using System;
using System.Collections.Generic;
using System.Text;

namespace CSharp8
{
    public class RGBColor
    {
        public RGBColor(int red, int green, int blue)
        {
            R = red;
            G = green;
            B = blue;
        }

        public RGBColor(Rainbow colorBand)
        {
            switch (colorBand)
            {
                case Rainbow.Red:
                    (R, G, B) = (0xFF, 0x00, 0x00);
                    break;
                case Rainbow.Orange:
                    (R, G, B) = (0xFF, 0x7F, 0x00);
                    break;
                case Rainbow.Yellow:
                    (R, G, B) = (0xFF, 0xFF, 0x00);
                    break;
                case Rainbow.Green:
                    (R, G, B) = (0x00, 0xFF, 0x00);
                    break;
                case Rainbow.Blue:
                    (R, G, B) = (0x00, 0x00, 0xFF);
                    break;
                case Rainbow.Indigo:
                    (R, G, B) = (0x4B, 0x00, 0x82);
                    break;
                case Rainbow.Violet:
                    (R, G, B) = (0x94, 0x00, 0xD3);
                    break;
                default:
                    throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand));
            }
        }

        public int R { get; } = 0;
        public int G { get; } = 0;
        public int B { get; } = 0;

        public void Deconstruct(out int r, out int g, out int b) => (r, g, b) = (R, G, B);

        public static RGBColor FromRainbow(Rainbow colorBand) =>
            colorBand switch
            {
                Rainbow.Red => new RGBColor(0xFF, 0x00, 0x00),
                Rainbow.Orange => new RGBColor(0xFF, 0x7F, 0x00),
                Rainbow.Yellow => new RGBColor(0xFF, 0xFF, 0x00),
                Rainbow.Green => new RGBColor(0x00, 0xFF, 0x00),
                Rainbow.Blue => new RGBColor(0x00, 0x00, 0xFF),
                Rainbow.Indigo => new RGBColor(0x4B, 0x00, 0x82),
                Rainbow.Violet => new RGBColor(0x94, 0x00, 0xD3),
                _ => throw new ArgumentException(message: "invalid enum value", paramName: nameof(colorBand)),
            };
    }
}
