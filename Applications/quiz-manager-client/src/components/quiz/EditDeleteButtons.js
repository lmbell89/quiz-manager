import Button from "react-bootstrap/Button";
import { useNavigate } from "react-router-dom";
import PropTypes from "prop-types";

const EditDeleteButtons = ({ id, quizId, onDelete }) => {
  const navigate = useNavigate();
  const editOnClick = () => navigate("/question", { state: { id, quizId } });

  return (
    <div className="d-flex gap-2 mt-4">
      <Button onClick={editOnClick}>Edit Question</Button>
      <Button onClick={() => onDelete(id)} variant="danger">
        Delete
      </Button>
    </div>
  );
};

EditDeleteButtons.propTypes = {
  id: PropTypes.number,
  quizId: PropTypes.number,
  onDelete: PropTypes.func,
};

export default EditDeleteButtons;
