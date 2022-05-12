import Spinner from "react-bootstrap/Spinner";

const Loading = () => {
  return (
    <div className="text-center w-100 pt-5">
      <Spinner animation="border" role="status">
        <span className="visually-hidden">Loading...</span>
      </Spinner>
    </div>
  );
};

export default Loading;
