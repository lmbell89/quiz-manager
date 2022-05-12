import Button from "react-bootstrap/Button";
import PropTypes from "prop-types";
import { useRoles } from "../../../utils/custom-hooks";

const TitleHeader = ({ text, setEditing }) => {
  const roles = useRoles();

  let editButton;
  // eslint-disable-next-line no-undef
  if (roles.includes(process.env.REACT_APP_EDIT_PERMISSION)) {
    editButton = <Button onClick={() => setEditing(true)}>Edit</Button>;
  }

  return (
    <>
      <h1>{text}</h1>
      {editButton}
    </>
  );
};

TitleHeader.propTypes = {
  text: PropTypes.string,
  setEditing: PropTypes.func,
};

export default TitleHeader;
