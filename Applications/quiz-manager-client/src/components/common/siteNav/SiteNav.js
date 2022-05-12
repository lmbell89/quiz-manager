import { Navbar, Container } from "react-bootstrap";
import LogoutButton from "./Logout";

const SiteNav = () => {
  return (
    <Navbar bg="light">
      <Container>
        <Navbar.Brand href="/">Quiz Manager</Navbar.Brand>
        <LogoutButton />
      </Container>
    </Navbar>
  );
};

export default SiteNav;
