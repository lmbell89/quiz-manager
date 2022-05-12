import { useState, useEffect } from "react";
import useApi from "../../api/api-client";
import { useAuth0 } from "@auth0/auth0-react";
import Loading from "../common/Loading";
import QuizSummary from "./QuizSummary";
import Search from "./Search";
import CreateButton from "./CreateButton";
import { useRoles } from "../../utils/custom-hooks";

const List = () => {
  const [quizzes, setQuizzes] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState("");

  const { getAccessTokenSilently } = useAuth0();
  const { getQuizzes, deleteQuiz } = useApi();
  const roles = useRoles();

  // eslint-disable-next-line no-undef
  const canCreate = roles.includes(process.env.REACT_APP_EDIT_PERMISSION);

  const _getQuizzes = (searchTerm) => {
    setIsLoading(true);
    getAccessTokenSilently()
      .then((token) => getQuizzes(token, searchTerm))
      .then((response) => {
        setIsLoading(false);
        setQuizzes(response);
      });
  };

  const _deleteQuiz = (id) => {
    getAccessTokenSilently()
      .then((token) => deleteQuiz(token, id))
      .then(() => _getQuizzes(searchTerm));
  };

  useEffect(_getQuizzes, []);

  if (isLoading) {
    return <Loading />;
  }

  return (
    <>
      {canCreate ? <CreateButton /> : null}
      <Search
        doSearch={_getQuizzes}
        searchTerm={searchTerm}
        setSearchTerm={setSearchTerm}
      />
      {quizzes.map((quiz) => (
        <QuizSummary
          key={quiz.id}
          id={quiz.id}
          title={quiz.title}
          questionCount={quiz.questions.length}
          doDelete={_deleteQuiz}
        />
      ))}
    </>
  );
};

export default List;
