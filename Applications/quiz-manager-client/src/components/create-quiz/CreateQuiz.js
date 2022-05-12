import { useState } from "react";
import Card from "react-bootstrap/Card";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import { useAuth0 } from "@auth0/auth0-react";
import { useNavigate } from "react-router-dom";
import useApi from "../../api/api-client";
import LinkButton from "../common/LinkButton";

const CreateQuiz = () => {
  const [title, setTitle] = useState("");
  const [showValidation, setShowValidation] = useState(false);

  const { createQuiz } = useApi();
  const { getAccessTokenSilently } = useAuth0();
  const navigate = useNavigate();

  const onSubmit = (e) => {
    setShowValidation(true);
    e.preventDefault();
    e.stopPropagation();
    if (title) {
      getAccessTokenSilently()
        .then((token) => createQuiz(token, title))
        .then(() => navigate("/list"));
    }
  };

  return (
    <Card>
      <Card.Body>
        <Card.Title>Create Quiz</Card.Title>
        <Form className="mt-2" noValidate onSubmit={(e) => onSubmit(e)}>
          <Form.Group>
            <Form.Label htmlFor="title">Quiz Title</Form.Label>
            <Form.Control
              type="text"
              id="title"
              value={title}
              onChange={(e) => setTitle(e.currentTarget.value)}
              isInvalid={showValidation && title == ""}
            />
            <Form.Control.Feedback type="invalid">
              Please provide a valid title.
            </Form.Control.Feedback>
          </Form.Group>

          <div className="mt-3 d-flex gap-3">
            <Button variant="success" type="submit">
              Save
            </Button>
            <LinkButton variant="secondary" to="/list">
              Cancel
            </LinkButton>
          </div>
        </Form>
      </Card.Body>
    </Card>
  );
};

export default CreateQuiz;
