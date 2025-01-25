import React, { useCallback, useState } from "react";
import boy1 from "../../src/components/img/boy1.jpg";
import boy2 from "../../src/components/img/boy2.jpg";
import boy3 from "../../src/components/img/boy3.jpg";
import boy4 from "../../src/components/img/boy4.jpg";
import "./boys.css"; // Подключаем CSS

function Boys() {
  console.log("Boy component rendered.");

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

  return (
    <div className="boys-container">
      {[boy1, boy2, boy3, boy4].map((boy, index) => (
        <div key={index} className="boy-card">
          <img src={boy} alt={`Boy ${index + 1}`} className="boy-image" />
          <div>
            <p>Likes: {likes[index]}</p>
            <button onClick={() => incrementLikes(index)}>Add Like</button>
          </div>
        </div>
      ))}
    </div>
  );
}

export default Boys;
