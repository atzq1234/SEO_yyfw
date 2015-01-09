using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Thumbnail
{
    public Thumbnail(string name, byte[] data, byte[] originaldata)
    {
        this.Name = name;
        this.Data = data;
        this.OriginalData = originaldata;
    }
    private string name;
    public string Name
    {
        get
        {
            return this.name;
        }
        set
        {
            this.name = value;
        }
    }
    private byte[] thumbnail_data;
    public byte[] Data
    {
        get
        {
            return this.thumbnail_data;
        }
        set
        {
            this.thumbnail_data = value;
        }
    }
    private byte[] original_data;
    public byte[] OriginalData
    {
        get
        {
            return this.original_data;
        }
        set
        {
            this.original_data = value;
        }
    }
}
public static class ListThumbnail
{
    public static List<Thumbnail> ListThum = new List<Thumbnail>();
}
