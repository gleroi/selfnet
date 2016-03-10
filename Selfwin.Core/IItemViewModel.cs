using System;
using Selfnet;

namespace Selfwin.Core
{
    public interface IItemViewModel
    {
        Item Parameter { get; set; }
        string Title { get; }
        string SourceTitle { get; }
        string SourceIcon { get; }
        string SourceIconUrl { get; }
        string Link { get; }
        bool Unread { get; set; }
        bool Starred { get; set; }
        string Html { get; set; }
        string Content { get; }
    }
}