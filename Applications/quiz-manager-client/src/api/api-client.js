const baseUrl = "https://localhost:44341";

const _fetch = async (url, token, method, body) => {
  return await fetch(url, {
    method,
    body,
    mode: "cors",
    headers: new Headers({
      Authorization: `Bearer ${token}`,
      "Content-Type": "application/json",
    }),
  });
};

const _get = async (url, token) => {
  const response = await _fetch(url, token, "GET", null);
  return await response.json();
};

const _delete = async (url, token) => {
  await _fetch(url, token, "DELETE", null);
};

const _patch = async (url, token, body) => {
  const response = await _fetch(url, token, "PATCH", body);
  return await response.json();
};

const _post = async (url, token, body) => {
  const response = await _fetch(url, token, "POST", body);
  return await response.json();
};

const getQuiz = async (id, token) => {
  const url = `${baseUrl}/quiz/${id}`;
  return _get(url, token);
};

const getQuizzes = async (token, searchTerm) => {
  const url = `${baseUrl}/quiz?title=${searchTerm || ""}`;
  return _get(url, token);
};

const deleteQuiz = async (token, id) => {
  const url = `${baseUrl}/quiz/${id}`;
  return _delete(url, token);
};

const updateQuizTitle = async (token, id, title) => {
  const url = `${baseUrl}/quiz/${id}`;
  const body = JSON.stringify({ title });
  return _patch(url, token, body);
};

const createQuiz = async (token, title) => {
  const url = `${baseUrl}/quiz`;
  const body = JSON.stringify({ title });
  return _post(url, token, body);
};

const createQuestion = async (token, quizId, text, index, answers) => {
  const url = `${baseUrl}/question`;
  const body = JSON.stringify({ quizId, text, index, answers });
  return _post(url, token, body);
};

const updateQuestion = async (token, id, quizId, text, index, answers) => {
  const url = `${baseUrl}/question/${id}`;
  const body = JSON.stringify({ quizId, text, index, answers });
  return _patch(url, token, body);
};

const getQuestion = async (token, id) => {
  const url = `${baseUrl}/question/${id}`;
  return _get(url, token);
};

const deleteQuestion = async (token, id) => {
  const url = `${baseUrl}/question/${id}`;
  return _delete(url, token);
};

const useApi = () => {
  return {
    getQuiz,
    getQuizzes,
    deleteQuiz,
    updateQuizTitle,
    createQuiz,
    createQuestion,
    updateQuestion,
    getQuestion,
    deleteQuestion,
  };
};

export default useApi;
