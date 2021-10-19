using System;
using CompAndDel.Pipes;
using CompAndDel.Filters;
using TwitterUCU;

namespace CompAndDel
{
    class Program
    {
        static void Main(string[] args)
        {
            //Ejercicio 1
            PictureProvider pictureProvider = new PictureProvider();
            IPicture picture = pictureProvider.GetPicture(@"beer.jpg");
            PipeNull pipenull = new PipeNull();
            FilterNegative negative = new FilterNegative();
            PipeSerial serial_1 = new PipeSerial(negative, pipenull);
            IPicture negativefilter = serial_1.Send(picture);
            FilterGreyscale grey = new FilterGreyscale();
            PipeSerial serial_2 = new PipeSerial(grey, serial_1);
            IPicture greyfilter = serial_2.Send(picture);
            
            //Ejercicio 2
            pictureProvider.SavePicture(negativefilter, @"negativefilter.jpg");
            pictureProvider.SavePicture(greyfilter, @"greyfilter.jpg");

            //Ejercicio 3
            var twitter = new TwitterImage();
            Console.WriteLine(twitter.PublishToTwitter("", @"negativefilter.jpg"));

            //Ejercicio 4}
            FilterTweeter tweet = new FilterTweeter($@"{picture}", "");
            PipeSerial serial_3 = new PipeSerial(tweet, pipenull);
            FilterConditional Face = new FilterConditional();
            PipeSerial SerialTwitter = new PipeSerial(Face, serial_3);
            PipeConditionalFork pipeConditional = new PipeConditionalFork(SerialTwitter, serial_1);
        }
    }
}
