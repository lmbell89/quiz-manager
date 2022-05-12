import Card from "react-bootstrap/Card";
import PropTypes from "prop-types";

// eslint-disable-next-line no-unused-vars
const Question = ({ index, text, children }) => {
  return (
    <Card className="my-4">
      <Card.Body>
        <Card.Subtitle>Question {index}</Card.Subtitle>
        <Card.Title>{text}</Card.Title>
        {children}
      </Card.Body>
    </Card>
  );
};

Question.propTypes = {
  id: PropTypes.number,
  index: PropTypes.number,
  text: PropTypes.string,
  children: PropTypes.array,
};

export default Question;
