import Form from "react-bootstrap/Form";
import InputGroup from "react-bootstrap/InputGroup";
import PropTypes from "prop-types";

const AnswerInput = ({
  index,
  text,
  setText,
  isCorrect,
  setIsCorrect,
  isValid,
  validationMessage,
  showValidation,
}) => {
  return (
    <InputGroup className="mb-3">
      <InputGroup.Text>{String.fromCharCode(65 + index)}</InputGroup.Text>
      <Form.Control
        type="text"
        value={text}
        className="me-2 rounded"
        onChange={(e) => setText(e.currentTarget.value)}
        isInvalid={showValidation && !isValid}
      />
      <Form.Check
        type="switch"
        className="d-flex align-items-center"
        checked={isCorrect}
        onChange={(e) => setIsCorrect(e.currentTarget.checked)}
        aria-label="Answer is correct"
        isInvalid={showValidation && !isValid}
      />
      <Form.Control.Feedback type="invalid">
        {validationMessage}
      </Form.Control.Feedback>
    </InputGroup>
  );
};

AnswerInput.propTypes = {
  index: PropTypes.number,
  text: PropTypes.string,
  setText: PropTypes.func,
  isCorrect: PropTypes.bool,
  setIsCorrect: PropTypes.func,
  isValid: PropTypes.bool,
  validationMessage: PropTypes.string,
  showValidation: PropTypes.bool,
};

export default AnswerInput;
