import { Routes, Route } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import Login from "./components/login";
import List from "./components/list";
import Quiz from "./components/quiz";
import QuestionForm from "./components/question/QuestionForm";
import Layout from "./components/common/Layout";
import Loading from "./components/common/Loading";
import CreateQuiz from "./components/create-quiz/CreateQuiz";

function App() {
  const { isLoading, isAuthenticated } = useAuth0();

  if (isLoading) {
    <Loading />;
  }

  if (!isLoading && !isAuthenticated) {
    return <Login />;
  }

  return (
    <>
      <Layout>
        <Routes>
          <Route path="/quiz/:id" element={<Quiz />} />
          <Route path="/question" element={<QuestionForm />} />
          <Route path="/create-quiz" element={<CreateQuiz />} />
          <Route path="*" element={<List />} />
        </Routes>
      </Layout>
    </>
  );
}

export default App;
