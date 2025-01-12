import { ChangeEventHandler, useCallback, useState } from "react";
import "./AnimeGirl.css";

function AnimeGirl() {
  const [name, setName] = useState<string>("Liza");
  const [src, setSrc] = useState<string>(
    "https://pics.craiyon.com/2023-10-23/903eec12f88642079733f99cf3e1eb17.webp"
  );
  const [likeCount, setLikeCount] = useState<number>(0);

  const [isEditMode, setIsEditMode] = useState<boolean>(false);

  const updateSrc = useCallback((evt: React.ChangeEvent<HTMLInputElement>) => {
    const newSrc = evt.target.value;
    setSrc(newSrc);
  }, []);

  const [tags, setTags] = useState<string[]>(["girl"]);

  const [newTag, setNewTag] = useState<string>("");

  const updateNewTag = useCallback(
    (evt: React.ChangeEvent<HTMLInputElement>) => {
      setNewTag(evt.target.value);
    },
    []
  );

  const addTag = useCallback(() => {
    setTags((oldTags) => [...oldTags, newTag]);
  }, [newTag]);

  return (
    <div>
      <div className="girl">
        <div className="name">
          {name} [{likeCount}]
        </div>
        <div className="image-container">
          <img
            src={src}
            alt="girl"
            onClick={() => setIsEditMode((old) => !old)}
          />
        </div>
        {isEditMode && (
          <div>
            <input value={src} onChange={updateSrc} />
          </div>
        )}
        <div>
          <button onClick={() => setLikeCount((old) => old + 1)}>like</button>
        </div>
        <div className="tags">
          {tags.map((tag) => (
            <span className="tag" key={tag}>
              {tag}
            </span>
          ))}
        </div>
      </div>
      <div className="create-new-tag">
        <input value={newTag} onChange={updateNewTag} />
        <button onClick={addTag}>add tag</button>
      </div>
    </div>
  );
}

export default AnimeGirl;
