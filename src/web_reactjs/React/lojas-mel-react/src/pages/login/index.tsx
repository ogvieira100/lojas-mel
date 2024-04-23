import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { UserLogin } from '../../types/user';

const defaultTheme = createTheme();


const Login = () => {

  const [userLoginForm, setUserLoginForm] = React.useState<UserLogin>({ email: '', password: '' });
  const [formPasswordTouched, setFormPasswordTouched] = React.useState(false);
  const [formEmailTouched, setFormEmailTouched] = React.useState(false);
  const [formValid, setFormValid] = React.useState(false);

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!formValid)
      {
        setFormPasswordTouched(true);
        setFormEmailTouched(true);
        return
      }
    if (formValid) {
      console.log(userLoginForm)
    }

    //const data = new FormData(event.currentTarget);
    /*   console.log({
        email: data.get('email'),
        password: data.get('password'),
      }); */
  };

  const handlePasswordTouched = () => {
    setFormPasswordTouched(true);
  };

  const handleEmailTouched = () => {
    setFormEmailTouched(true);
  };

  React.useEffect(() => {


      if (userLoginForm.email != ''
       && userLoginForm.password != '')
        setFormValid(true);
      else
        setFormValid(false);
   
    () => { }

  }, [userLoginForm])



  return (
    <ThemeProvider theme={defaultTheme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 15,
            display: 'flex',
            flexDirection: 'column',
            alignItems: 'center',

          }}
        >
          <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Lojas Mel
          </Typography>
          <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
            <Grid container spacing={2}>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  id="email"
                  label="Email"
                  onBlur={handleEmailTouched} 
                  value={userLoginForm?.email}
                  name="email"
                  onChange={(e) => setUserLoginForm({ ...userLoginForm, email: e.currentTarget.value })}
                  autoComplete="email"
                  helperText={(userLoginForm?.email == '' && formEmailTouched) && "Atenção digite o email"}
                  error={(userLoginForm?.email == '' && formEmailTouched  )}
                />
              </Grid>
              <Grid item xs={12}>
                <TextField
                  fullWidth
                  name="password"
                  label="Senha"
                  type="password"
                  id="password"
                  onBlur={handlePasswordTouched} 
                  value={userLoginForm?.password}
                  onChange={(e) => setUserLoginForm({ ...userLoginForm, password: e.currentTarget.value })}
                  autoComplete="new-password"
                  helperText={(userLoginForm?.password == '' && formPasswordTouched) && "Atenção digite a senha"}
                  error={(userLoginForm?.password == ''  && formPasswordTouched)}
                />
              </Grid>
            </Grid>
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Entrar
            </Button>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>)

}
export default Login

