import React, { useState } from "react";

function Likes() {
  console.log("Likes component rendered. And added like.");

  const [like, setNumber] = useState<number>(0); // Инициализация состояния

  // Функция для увеличения числа "лайков"
  const incrementLikes = () => {
    setNumber((prevLike) => prevLike + 1); // Обновление состояния
  };

  return (
    <div>
      <p>Likes: {like}</p>
      <button onClick={incrementLikes}>Add Like</button>
    </div>
  );
}

export default Likes;
