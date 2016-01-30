using System;
using System.Collections.Generic;
using System.Text;

namespace Meow.FR.Rincevent.Core.Extensibility
{
    /// <summary>
    /// Interface to access the core from the modules
    /// </summary>
    public interface ICore
    {
        string FileName { get; }

        /// <summary>
        /// Gets the playlists.
        /// </summary>
        /// <returns></returns>
        string[] Playlists { get; }

        /// <summary>
        /// Creates the playlist.
        /// </summary>
        /// <returns>The name of the created playlist.</returns>
        string CreatePlaylist();

        /// <summary>
        /// Adds index to the specified playlist.
        /// </summary>
        /// <param name="playlistName">Name of the playlist.</param>
        /// <param name="indexToAdd">The index to add.</param>
        void AddToPlaylist(string playlistName, int indexToAdd);

        /// <summary>
        /// Removes index from the playlist.
        /// </summary>
        /// <param name="playlistName">Name of the playlist.</param>
        /// <param name="indexToRemove">The index to remove.</param>
        void RemoveFromPlaylist(string playlistName, int indexToRemove);

        /// <summary>
        /// Checks the specified index.
        /// </summary>
        /// <param name="indexToCheck">The index to check.</param>
        void Check(int indexToCheck);

        /// <summary>
        /// Unchecks the specified index.
        /// </summary>
        /// <param name="indexToUncheck">The index to uncheck.</param>
        void Uncheck(int indexToUncheck);

        /// <summary>
        /// Calls the main Rincevent interface to come up.
        /// </summary>
        void WakeUp();
    }
}
