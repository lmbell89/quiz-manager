import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router";
import { useAuth0 } from "@auth0/auth0-react";
import Loading from "../common/Loading";
import Question from "./Question";
import QuizTitle from "./title/QuizTitle";
import useApi from "../../api/api-client";
import { useRoles } from "../../utils/custom-hooks";
import Button from "react-bootstrap/Button";
import CreateQuestionButton from "./CreateQuestionButton";
import AnswerGroup from "./AnswerGroup";
import EditDeleteButtons from "./EditDeleteButtons";

const Quiz = () => {
  const [quiz, setQuiz] = useState();
  const [isLoading, setIsLoading] = useState(true);

  const { getQuiz, deleteQuestion } = useApi();
  const roles = useRoles();
  const { getAccessTokenSilently } = useAuth0();
  const params = useParams();
  const navigate = useNavigate();

  const quizId = parseInt(params.id);
  // eslint-disable-next-line no-undef
  const hasEditRole = roles.includes(process.env.REACT_APP_EDIT_PERMISSION);
  // eslint-disable-next-line no-undef
  const hasViewRole = roles.includes(process.env.REACT_APP_VIEW_PERMISSION);

  const loadQuiz = () => {
    getAccessTokenSilently()
      .then((token) => getQuiz(quizId, token))
      .then((quiz) => {
        setQuiz(quiz);
        setIsLoading(false);
      });
  };

  const onDelete = (id) => {
    let token;
    getAccessTokenSilently()
      .then((t) => (token = t))
      .then(() => deleteQuestion(token, id))
      .then(() => getQuiz(quizId, token))
      .then((quiz) => setQuiz(quiz));
  };

  useEffect(loadQuiz, [quizId]);

  if (isLoading) {
    return <Loading />;
  }

  return (
    <>
      <QuizTitle id={quizId} text={quiz.title} refreshQuiz={loadQuiz} />
      {quiz.questions.map((question) => {
        return (
          <Question
            id={question.id}
            key={question.id}
            index={question.details.index}
            text={question.details.text}
          >
            {hasViewRole || hasEditRole ? <hr /> : null}
            {hasViewRole || hasEditRole ? (
              <AnswerGroup answers={question.details.answers} />
            ) : null}
            {hasEditRole ? (
              <EditDeleteButtons
                id={question.id}
                quizId={quizId}
                onDelete={onDelete}
              />
            ) : null}
          </Question>
        );
      })}
      <div className="p-3 mt-2 d-flex gap-3">
        {hasEditRole ? <CreateQuestionButton quizId={quizId} /> : null}
        <Button variant="secondary" onClick={() => navigate(-1)}>
          Back
        </Button>
      </div>
    </>
  );
};

export default Quiz;
