﻿namespace BlogApp.Api.Commons.Helpers
{
    public class FileSizeHelper
    {
        public static double ByteToMB(double @byte)
        {
            return @byte / 1024 / 1024;
        }
    }
}