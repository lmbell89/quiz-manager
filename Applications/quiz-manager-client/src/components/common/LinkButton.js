import { Link } from "react-router-dom";
import PropTypes from "prop-types";

// eslint-disable-next-line react/prop-types
const LinkButton = ({ children, to, variant }) => {
  const className = `btn btn-${variant}`;
  return (
    <Link className={className} to={to}>
      {children}
    </Link>
  );
};

LinkButton.propTypes = {
  to: PropTypes.string,
  variant: PropTypes.string,
};

export default LinkButton;
