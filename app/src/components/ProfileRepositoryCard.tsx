import { Typography, Button, Card, CardActions, CardContent } from '@mui/material';
import { ProfileRepositoryResponse, RepositoryValidationProgressStatus } from '../api/client/ScarecrowClient';
import api from '../api/core/ApiClient';
import { useNavigate } from 'react-router-dom';

interface ProfileRepositoryCardResponseProps {
  profileName: string;
  repository: ProfileRepositoryResponse;
}

export default function ProfileRepositoryCardResponse({ profileName, repository }: ProfileRepositoryCardResponseProps) {
  const navigate = useNavigate();

  return (
    <Card sx={{ minWidth: 275 }}>
      <CardContent>
        <Typography sx={{ fontSize: 14 }} color="text.secondary" gutterBottom>
          Repository
        </Typography>
        <Typography variant="h5" component="div" marginBottom="0.25em">
          {repository.name}
        </Typography>
        <Typography variant="body2">
          {getStatusLabel(repository.repositoryValidationProgressStatus)}
          <br />
          {repository.lastValidation && repository.isOk !== undefined && (
            <>
              Last validation: {repository.lastValidation ?? 'never'}
              <br />
              Result: {repository.isOk ? 'Ok' : 'Error'}
            </>
          )}
        </Typography>
      </CardContent>
      <CardActions>
        <Button size="small" onClick={() => navigate(repository.name)}>
          Details
        </Button>
        <Button size="small" onClick={() => window.open(repository.url, '_blank')}>
          open
        </Button>
        <Button size="small" onClick={() => validate(profileName, repository.name)}>
          Validate
        </Button>
      </CardActions>
    </Card>
  );
}

const getStatusLabel = (status: RepositoryValidationProgressStatus): JSX.Element => (
  <>
    Status: <span style={{ color: getColorByStatus(status) }}>{status}</span>
  </>
);

const getColorByStatus = (status: RepositoryValidationProgressStatus): string => {
  switch (status) {
    case RepositoryValidationProgressStatus.FINISHED:
      return 'green';
    case RepositoryValidationProgressStatus.IN_PROGRESS:
      return 'yellow';
    case RepositoryValidationProgressStatus.NEVER_DONE:
      return 'orange';
  }
};

const validate = (profile: string, repo: string) => api.profileRepositories.postValidationByNameRepoName(profile, repo);
