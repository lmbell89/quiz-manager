import PropTypes from "prop-types";
import Form from "react-bootstrap/Form";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSearch } from "@fortawesome/free-solid-svg-icons";
import styles from "./Search.module.css";

const Search = ({ doSearch, searchTerm, setSearchTerm }) => {
  const onSubmit = (e) => {
    e.preventDefault();
    doSearch(searchTerm);
  };

  return (
    <Form onSubmit={onSubmit} className={styles.container + " my-4"}>
      <Form.Control
        type="text"
        className={styles.input + " rounded-pill bg-white"}
        value={searchTerm}
        onChange={(e) => setSearchTerm(e.currentTarget.value)}
      />
      <FontAwesomeIcon icon={faSearch} className={styles.icon} />
    </Form>
  );
};

Search.propTypes = {
  doSearch: PropTypes.func,
  searchTerm: PropTypes.string,
  setSearchTerm: PropTypes.func,
};

export default Search;
