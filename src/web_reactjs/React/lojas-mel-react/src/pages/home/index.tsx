import CssBaseline from '@mui/material/CssBaseline';
import Box from '@mui/material/Box';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import EmojiNatureIcon from '@mui/icons-material/EmojiNature';

const Home = () => {
    const defaultTheme = createTheme();
    return (<>
        <ThemeProvider theme={defaultTheme}>
            <Container  component="main" maxWidth="xs" >
                <CssBaseline />
                <Box
                    sx={{
                        display: 'flex',
                        flexDirection: 'row',
                        alignItems: 'center',
                        justifyContent:'center',
                        marginTop:'50%'
                    }}
                >
                     <EmojiNatureIcon style={{fontSize:'100'}}  />
                </Box>
            </Container>
        </ThemeProvider>

    </>)

}
export default Home;