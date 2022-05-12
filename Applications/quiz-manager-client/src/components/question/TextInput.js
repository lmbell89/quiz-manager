import Form from "react-bootstrap/Form";
import PropTypes from "prop-types";

const TextInput = ({ text, setText, isInvalid }) => {
  return (
    <Form.Group className="mb-3">
      <Form.Label>Question text</Form.Label>
      <Form.Control
        required
        type="text"
        value={text}
        onChange={(e) => setText(e.currentTarget.value)}
        isInvalid={isInvalid}
      />
      <Form.Control.Feedback type="invalid">
        Please provide text for the question.
      </Form.Control.Feedback>
    </Form.Group>
  );
};

TextInput.propTypes = {
  text: PropTypes.string,
  setText: PropTypes.func,
  isInvalid: PropTypes.bool,
};

export default TextInput;
