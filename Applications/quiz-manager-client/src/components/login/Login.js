import LoginButton from "./LoginButton";
import logo from "../../logo.svg";
import styles from "./Login.module.css";

const Login = () => {
  // eslint-disable-next-line no-undef
  const organisationName = process.env.REACT_APP_ORGANISATION;

  return (
    <div className={styles.loginContainer}>
      <img src={logo} alt="My logo" className={styles.logo} />
      <h1>{organisationName}</h1>
      <h2 className="text-muted mb-5">Quiz Manager</h2>
      <LoginButton />
    </div>
  );
};

export default Login;
