import SiteNav from "./siteNav";
import Container from "react-bootstrap/Container";
import Col from "react-bootstrap/Col";
import Row from "react-bootstrap/Row";

// eslint-disable-next-line react/prop-types
const Layout = ({ children }) => {
  return (
    <>
      <SiteNav />
      <Container className="my-3">
        <Row>
          <Col />
          <Col sm={12} md={8} lg={6}>
            {children}
          </Col>
          <Col />
        </Row>
      </Container>
    </>
  );
};

export default Layout;
