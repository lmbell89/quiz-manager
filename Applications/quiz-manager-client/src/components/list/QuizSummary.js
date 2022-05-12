import Card from "react-bootstrap/Card";
import PropTypes from "prop-types";
import LinkButton from "../common/LinkButton";
import { useRoles } from "../../utils/custom-hooks";
import Button from "react-bootstrap/Button";

const QuizSummary = ({ id, title, questionCount, doDelete }) => {
  const roles = useRoles();
  // eslint-disable-next-line no-undef
  const canEdit = roles.includes(process.env.REACT_APP_EDIT_PERMISSION);

  const deleteButton = (
    <Button variant="danger" onClick={() => doDelete(id)}>
      Delete
    </Button>
  );

  return (
    <Card className="my-4">
      <Card.Body>
        <Card.Title>{title}</Card.Title>
        <Card.Subtitle className="mb-2 text-muted">
          {questionCount == 1
            ? `${questionCount} question`
            : `${questionCount} questions`}
        </Card.Subtitle>
        <hr />
        <div className="d-flex gap-3">
          <LinkButton to={`/quiz/${id}`} variant="primary">
            {canEdit ? "Edit" : "View"}
          </LinkButton>
          {canEdit ? deleteButton : null}
        </div>
      </Card.Body>
    </Card>
  );
};

QuizSummary.propTypes = {
  id: PropTypes.number,
  title: PropTypes.string,
  questionCount: PropTypes.number,
  doDelete: PropTypes.func,
};

export default QuizSummary;
