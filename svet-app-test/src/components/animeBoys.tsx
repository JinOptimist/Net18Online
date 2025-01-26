import React, { useCallback, useState } from "react";
import boy1 from "../../src/components/img/boy1.jpg";
import boy2 from "../../src/components/img/boy2.jpg";
import boy3 from "../../src/components/img/boy3.jpg";
import boy4 from "../../src/components/img/boy4.jpg";
import "./boys.css"; // Подключаем CSS

function Boys() {
  console.log("Boy component rendered.");

  // Состояния для изображений и их имен
  const [images, setImages] = useState<string[]>([boy1, boy2, boy3, boy4]);
  const [names, setNames] = useState<string[]>([
    "Boy 1",
    "Boy 2",
    "Boy 3",
    "Boy 4",
  ]);

  // Состояния для лайков каждого изображения
  const [likes, setLikes] = useState<number[]>([0, 0, 0, 0]);

  // Функция для обновления лайков
  const incrementLikes = useCallback((index: number) => {
    setLikes((prevLikes) => {
      const updatedLikes = [...prevLikes];
      updatedLikes[index] += 1;
      return updatedLikes;
    });
  }, []);

  // Функция для обновления имени
  const updateName = useCallback((index: number, newName: string) => {
    setNames((prevNames) => {
      const updatedNames = [...prevNames];
      updatedNames[index] = newName; // Обновляем конкретное имя
      return updatedNames;
    });
  }, []);

  return (
    <div className="boys-container">
      {images.map((src, index) => (
        <div key={index} className="boy-card">
          <img src={src} alt={`Boy ${index + 1}`} className="boy-image" />
          <div>
            <p>Likes: {likes[index]}</p>
            <button onClick={() => incrementLikes(index)}>Add Like</button>
          </div>
          <div>
            <p>Name: {names[index]}</p>
            <input
              type="text"
              value={names[index]}
              onChange={(e) => updateName(index, e.target.value)} // Обновляем имя
              placeholder="Enter new name"
            />
          </div>
        </div>
      ))}
    </div>
  );
}

export default Boys;
