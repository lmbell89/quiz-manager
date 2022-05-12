import { useNavigate } from "react-router";
import Button from "react-bootstrap/Button";
import PropTypes from "prop-types";

const CreateQuestionButton = ({ quizId }) => {
  const navigate = useNavigate();

  const onClick = () => {
    navigate("/question", { state: { quizId } });
  };

  return (
    <Button variant="success" onClick={onClick}>
      Create Question
    </Button>
  );
};

CreateQuestionButton.propTypes = {
  quizId: PropTypes.number,
};

export default CreateQuestionButton;
