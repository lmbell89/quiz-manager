import { useState } from "react";
import Card from "react-bootstrap/Card";
import PropTypes from "prop-types";
import TitleForm from "./TitleForm";
import TitleHeader from "./TitleHeader";

const QuizTitle = ({ id, text, refreshQuiz }) => {
  const [editing, setEditing] = useState(false);

  let body;
  if (editing) {
    body = (
      <TitleForm
        id={id}
        text={text}
        setEditing={setEditing}
        refreshQuiz={refreshQuiz}
      />
    );
  } else {
    body = <TitleHeader text={text} setEditing={setEditing} />;
  }

  return (
    <Card>
      <Card.Body>
        <Card.Subtitle>Title</Card.Subtitle>
        {body}
      </Card.Body>
    </Card>
  );
};

QuizTitle.propTypes = {
  id: PropTypes.number,
  text: PropTypes.string,
  refreshQuiz: PropTypes.func,
};

export default QuizTitle;
