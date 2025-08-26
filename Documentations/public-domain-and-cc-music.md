# Public Domain and Creative Commons Music

Using music that is either in the public domain or released under a Creative Commons (CC) license avoids many of the legal risks of distributing copyrighted material. Services like [Free Music Archive](https://freemusicarchive.org/) and [Jamendo](https://www.jamendo.com/start) expose catalogs of tracks that can be streamed or downloaded under permissive terms.

## Implementation Notes
- Integrate with an open catalog's API (for example, FMA or Jamendo) to let users search for tracks that are already cleared for reuse.
- Store license metadata with each quiz entry so the app can display proper attribution when necessary.
- Filter tracks by license to only allow those that permit reuse and redistribution in a quiz context.
- Cache short clips locally to reduce bandwidth while respecting the original hosting provider's terms.

## Advantages
- Large library of free tracks spanning many genres.
- Minimal legal exposure as long as license terms (e.g., attribution) are respected.
- Users gain immediate access to music without providing their own files.

## Considerations
- Some CC licenses (like BY-NC) may prohibit commercial use. The app should let quiz creators review the license before selecting a track.
- Attribution text or links may need to appear alongside quiz questions.
