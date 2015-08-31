var CommentList = React.createClass({
  propTypes: {
    comments: React.PropTypes.array.isRequired
  },

  render() {
    var commentNodes = this.props.comments.map(function(comment) {
      return <Comment author={comment.author}>{comment.text}</Comment>
    });

    return (
      <div className="comments">
        <h1>Comments</h1>
        <ul className="commentList">
            {commentNodes}
        </ul>
      </div>
    );
  }
});