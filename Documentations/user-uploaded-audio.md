# User-Uploaded Audio

Allowing users to upload their own audio clips gives them full control over quiz content while keeping the app provider out of the copyright chain. The quiz itself only references files stored on the creator's device or a personal cloud service.

For the initial release the goal is to let every quiz stay portable: creators can point to audio that already exists on the recipient's device or bundle small clips directly with the quiz. This can be achieved by extending the existing `ResourceProvider` system so multiple sources of audio are supported transparently.

## Implementation Notes
- Provide an in-app file picker/recorder that lets users select MP3 or WAV files stored locally, record new clips, or import from common cloud services.
- Expand the `ResourceProvider` abstraction to support several ways of resolving audio:
  - **WebResourceProvider** – existing URLs to publicly reachable MP3 files.
  - **ResourcesFolderProvider** – audio bundled inside the app’s `Resources` directory for offline sharing.
  - **AddressablesProvider** – built‑in quiz packs distributed via Unity addressables.
  - **AttachmentProvider** – embed small audio files (for example as base64 strings) directly inside the serialized quiz so the file travels with the quiz.
- When sharing a quiz, transmit only metadata (file hashes, provider type, and optional URLs). Recipients resolve the audio via the same providers; if a file is missing they are prompted to supply or download it.
- Offer helpers to upload missing clips to personal cloud storage and generate a shareable link that can be used by the `WebResourceProvider`.
- Display clear terms requiring users to upload content only if they have the necessary rights.
- Optional: implement fingerprinting or length limits to discourage full‑song uploads.

## Advantages
- Maximum flexibility: creators can use any audio they legally control.
- No reliance on third-party streaming APIs.
- The app avoids hosting or distributing potentially infringing files.

## Considerations
- Users may still violate copyright by uploading material they do not own; the app should include disclaimers and a takedown policy.
- Quizzes shared online might fail if recipients do not possess the referenced audio files; fallback mechanisms or clear error messages are needed.
