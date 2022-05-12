import { useAuth0 } from "@auth0/auth0-react";

const useRoles = () => {
  const { user } = useAuth0();
  // eslint-disable-next-line no-undef
  return user?.[process.env.REACT_APP_AUTH0_ROLES_KEY] || [];
};

export { useRoles };
