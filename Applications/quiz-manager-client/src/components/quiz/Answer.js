import ListGroup from "react-bootstrap/ListGroup";
import PropTypes from "prop-types";

const Answer = ({ text, isCorrect }) => {
  return (
    <ListGroup.Item variant={isCorrect ? "success" : "light"}>
      {text}
    </ListGroup.Item>
  );
};

Answer.propTypes = {
  text: PropTypes.string,
  isCorrect: PropTypes.bool,
};

export default Answer;
