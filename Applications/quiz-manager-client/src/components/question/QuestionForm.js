import { useState, useEffect } from "react";
import Card from "react-bootstrap/Card";
import Button from "react-bootstrap/Button";
import Form from "react-bootstrap/Form";
import PropTypes from "prop-types";
import { useNavigate, useLocation } from "react-router-dom";
import TextInput from "./TextInput";
import NumberInput from "./NumberInput";
import AnswerInputs from "./AnswerInputs";
import useApi from "../../api/api-client";
import { useAuth0 } from "@auth0/auth0-react";

// eslint-disable-next-line no-unused-vars
const QuestionForm = () => {
  const [validated, setValidated] = useState(false);
  const [title, setTitle] = useState("");
  const [number, setNumber] = useState(1);
  const [answers, setAnswers] = useState([]);

  const navigate = useNavigate();
  const { state } = useLocation();
  const { getAccessTokenSilently } = useAuth0();
  const { createQuestion, updateQuestion, getQuestion } = useApi();

  const quizId = state?.quizId;
  const questionId = state?.id;

  if (!quizId) {
    navigate("/list");
  }

  useEffect(() => {
    if (questionId) {
      getAccessTokenSilently()
        .then((token) => getQuestion(token, questionId))
        .then((question) => {
          setTitle(question.details.text);
          setNumber(question.details.index);
          setAnswers(question.details.answers);
        });
    }
  }, []);

  const handleSubmit = (e) => {
    const hasMinimumAnswers = answers.slice(0, 3).every((a) => a.text);
    const hasSomeIncorrect = answers.some((a) => a.text && !a.isCorrect);
    const hasSomeCorrect = answers.some((a) => a.text && a.isCorrect);
    const firstBlankIndex = answers.findIndex((a) => !a.text);
    const hasNoBreaks = answers.every((a, i) => !a.text && i > firstBlankIndex);
    const areAnswersValid =
      hasMinimumAnswers && hasSomeIncorrect && hasSomeCorrect && hasNoBreaks;

    e.preventDefault();
    e.stopPropagation();
    setValidated(true);

    if (areAnswersValid && title && number >= 1) {
      return;
    }

    getAccessTokenSilently().then((token) => {
      if (questionId) {
        updateQuestion(token, questionId, quizId, title, number, answers).then(
          () => navigate(-1)
        );
      } else {
        createQuestion(token, quizId, title, number, answers).then(() =>
          navigate(-1)
        );
      }
    });
  };

  return (
    <Card>
      <Card.Body>
        <Card.Title className="mb-4">
          {state?.id ? "Edit question" : "Create question"}
        </Card.Title>
        <Form noValidate onSubmit={handleSubmit}>
          <TextInput
            text={title}
            setText={setTitle}
            isInvalid={validated && !title}
          />
          <NumberInput
            value={number}
            setValue={setNumber}
            isInvalid={validated && number < 1}
          />
          <AnswerInputs
            answers={answers}
            setAnswers={setAnswers}
            showValidation={validated}
          />

          <div className="d-flex gap-3 mt-5">
            <Button variant="success" type="submit">
              Save question
            </Button>
            <Button variant="warning" onClick={() => navigate(-1)}>
              Cancel
            </Button>
          </div>
        </Form>
      </Card.Body>
    </Card>
  );
};

QuestionForm.propTypes = {
  id: PropTypes.number,
  text: PropTypes.string,
  index: PropTypes.number,
};

export default QuestionForm;
