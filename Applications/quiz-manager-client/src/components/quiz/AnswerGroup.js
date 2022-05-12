import ListGroup from "react-bootstrap/ListGroup";
import PropTypes from "prop-types";
import Answer from "./Answer";

const AnswerGroup = ({ answers }) => {
  const getAnswerText = (index, text) =>
    String.fromCharCode(index + 65) + ". " + text;

  return (
    <ListGroup>
      {answers.map((answer, index) => (
        <Answer
          key={answer.id}
          text={getAnswerText(index, answer.text)}
          isCorrect={answer.isCorrect}
        />
      ))}
    </ListGroup>
  );
};

AnswerGroup.propTypes = {
  answers: PropTypes.arrayOf(
    PropTypes.shape({
      id: PropTypes.number,
      text: PropTypes.string,
      isCorrect: PropTypes.bool,
    })
  ),
};

export default AnswerGroup;
