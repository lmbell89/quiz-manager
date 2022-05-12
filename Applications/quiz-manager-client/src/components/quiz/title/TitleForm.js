import { useState } from "react";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import PropTypes from "prop-types";
import useApi from "../../../api/api-client";
import { useAuth0 } from "@auth0/auth0-react";

// eslint-disable-next-line no-unused-vars
const TitleForm = ({ id, text, setEditing, refreshQuiz }) => {
  const [title, setTitle] = useState(text);

  const { getAccessTokenSilently } = useAuth0();
  const { updateQuizTitle } = useApi();

  const onSubmit = (e) => {
    if (!title) {
      e.preventDefault();
      e.stopPropagation();
    } else {
      setEditing(false);
      getAccessTokenSilently()
        .then((token) => updateQuizTitle(token, id, title))
        .then(refreshQuiz);
    }
  };

  return (
    <Form className="mt-2" noValidate onSubmit={(e) => onSubmit(e)}>
      <Form.Group>
        <Form.Label className="visually-hidden" htmlFor="title">
          Quiz Title
        </Form.Label>
        <Form.Control
          type="text"
          id="title"
          value={title}
          onChange={(e) => setTitle(e.currentTarget.value)}
          isInvalid={title == ""}
          required
        />
        <Form.Control.Feedback type="invalid">
          Please provide a valid title.
        </Form.Control.Feedback>
      </Form.Group>

      <div className="mt-2 d-flex gap-3">
        <Button variant="success" type="submit">
          Save
        </Button>
        <Button variant="warning" onClick={() => setEditing(false)}>
          Cancel
        </Button>
      </div>
    </Form>
  );
};

TitleForm.propTypes = {
  id: PropTypes.number,
  text: PropTypes.string,
  setEditing: PropTypes.func,
  refreshQuiz: PropTypes.func,
};

export default TitleForm;
