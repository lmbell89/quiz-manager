import Form from "react-bootstrap/Form";
import PropTypes, { oneOfType } from "prop-types";

const NumberInput = ({ value, setValue, isInvalid }) => {
  return (
    <Form.Group className="mb-3">
      <Form.Label>Question number</Form.Label>
      <Form.Control
        required
        type="number"
        value={value}
        onChange={(e) => setValue(e.currentTarget.value)}
        isInvalid={isInvalid}
      />
      <Form.Control.Feedback type="invalid">
        Please enter a number greater than zero.
      </Form.Control.Feedback>
    </Form.Group>
  );
};

NumberInput.propTypes = {
  value: oneOfType([PropTypes.number, PropTypes.string]),
  setValue: PropTypes.func,
  isInvalid: PropTypes.bool,
};

export default NumberInput;
