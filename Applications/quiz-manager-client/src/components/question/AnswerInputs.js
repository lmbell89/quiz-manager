import AnswerInput from "./AnswerInput";
import PropTypes from "prop-types";
import Form from "react-bootstrap/Form";

const AnswerInputs = ({ answers, setAnswers, showValidation }) => {
  const updateAnswers = (text, isCorrect, index) => {
    const copyOfAnswers = [...answers];
    const updatedAnswer = { ...copyOfAnswers[index], text, isCorrect };
    copyOfAnswers[index] = updatedAnswer;
    setAnswers(copyOfAnswers);
  };

  const setText = (index, isCorrect) => {
    return (text) => updateAnswers(text, isCorrect, index);
  };

  const setIsCorrect = (index, text) => {
    return (isCorrect) => updateAnswers(text, isCorrect, index);
  };

  let inputs = [];

  for (let i = 0; i < 5; i++) {
    let isValid = true;
    let validationMessage = "";

    const firstBlankIndex = answers.findIndex((a) => !a?.text);
    const hasMinimumAnswers = firstBlankIndex == -1 || firstBlankIndex >= 3;

    if (i < 3 && !hasMinimumAnswers && !answers[i]?.text) {
      isValid = false;
      validationMessage = "Please enter some text for this answer.";
    }
    if (
      i >= 3 &&
      !answers[i]?.text &&
      answers.find((a, idx) => a?.text && idx > i)
    ) {
      isValid = false;
      validationMessage =
        "Please enter some text for this answer or delete all later answers.";
    }
    if (answers.filter((a) => a?.text).every((a) => a?.isCorrect)) {
      isValid = false;
      validationMessage = "Please enter at least one incorrect answer.";
    }
    if (answers.filter((a) => a?.text).every((a) => !a?.isCorrect)) {
      isValid = false;
      validationMessage = "Please enter at least one correct answer.";
    }

    inputs.push(
      <AnswerInput
        key={i}
        index={i}
        text={answers[i]?.text || ""}
        isCorrect={answers[i]?.isCorrect || false}
        setText={setText(i, answers[i]?.isCorrect)}
        setIsCorrect={setIsCorrect(i, answers[i]?.text)}
        isValid={isValid}
        validationMessage={validationMessage}
        showValidation={showValidation}
      />
    );
  }

  return (
    <>
      <div className="d-flex justify-content-between mt-4">
        <Form.Label>Answers</Form.Label>
        <Form.Label>Correct</Form.Label>
      </div>
      {inputs}
    </>
  );
};

AnswerInputs.propTypes = {
  answers: PropTypes.array,
  setAnswers: PropTypes.func,
  showValidation: PropTypes.bool,
};

export default AnswerInputs;
