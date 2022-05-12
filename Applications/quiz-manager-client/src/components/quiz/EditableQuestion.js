import Button from "react-bootstrap/Button";
import Card from "react-bootstrap/Card";
import { useNavigate } from "react-router-dom";
import PropTypes from "prop-types";
import AnswerGroup from "./AnswerGroup";

const EditableQuestion = ({ id, quizId, index, text, answers, onDelete }) => {
  const navigate = useNavigate();
  const editOnClick = () => navigate("/question", { state: { id, quizId } });

  return (
    <Card className="my-4">
      <Card.Body>
        <Card.Subtitle>Question {index}</Card.Subtitle>
        <Card.Title>{text}</Card.Title>
        <hr />
        <AnswerGroup answers={answers} />
        <div className="d-flex gap-2 mt-4">
          <Button onClick={editOnClick}>Edit Question</Button>
          <Button onClick={() => onDelete(id)} variant="danger">
            Delete
          </Button>
        </div>
      </Card.Body>
    </Card>
  );
};

EditableQuestion.propTypes = {
  id: PropTypes.number,
  quizId: PropTypes.number,
  index: PropTypes.number,
  text: PropTypes.string,
  answers: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number,
      text: PropTypes.string,
      isCorrect: PropTypes.bool,
    })
  ),
  onDelete: PropTypes.func,
};

export default EditableQuestion;
