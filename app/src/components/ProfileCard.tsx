import { Typography, Button, Card, CardActions, CardContent } from '@mui/material';
import { ProfileResponse } from '../api/client/ScarecrowClient';
import { useNavigate } from 'react-router-dom';

interface ProfileCardProps {
  profile: ProfileResponse;
}

function ProfileCard({ profile }: ProfileCardProps) {
  const navigate = useNavigate();

  return (
    <Card sx={{ minWidth: 275 }}>
      <CardContent>
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          Profile
        </Typography>
        <Typography variant="h5" component="div" marginBottom="0.25em">
          {profile.name}
        </Typography>
        <Typography variant="body2">
          Rules: {profile.rules?.length}
          <br />
          Repositories: {profile.repositories?.length}
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small" onClick={() => navigate('profiles/' + profile.name)}>
          Details
        </Button>
      </CardActions>
    </Card>
  );
}

export default ProfileCard;
